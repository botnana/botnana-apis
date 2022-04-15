use std::sync::{Arc, Mutex};
use triple_buffer::{Input, Output};

// Size of modbus input and holding block in 16bit words.
pub const MB_BLOCK_SIZE: usize = 384;

#[derive(Debug)]
pub enum Error {
    InvalidAddr,
}

#[derive(Clone)]
pub struct ClientTable {
    coil_count: usize,
    din_count: usize,
    // Input registers
    // TODO: 改 Vec<u16> 為 ByteMut
    pub inputs: Arc<Mutex<Output<Vec<u16>>>>,
    // Holding registers
    pub holdings: Arc<Mutex<Input<Vec<u16>>>>,
}

impl ClientTable {
    /// Create a modbus table with a input block of 16-bit words and a holding block of 16-bit words.
    /// With first `coil_size` 16-bit words of holding block for coils and first `din_size` 16-bit words of input block for
    /// discrete inputs.
    pub fn new(
        inputs: Arc<Mutex<Output<Vec<u16>>>>,
        holdings: Arc<Mutex<Input<Vec<u16>>>>,
    ) -> ClientTable {
        let coil_count = holdings.lock().expect("Mb holdings").input_buffer().len();
        let din_count = inputs.lock().expect("Mb inputs").output_buffer().len();
        ClientTable {
            coil_count,
            din_count,
            inputs,
            holdings,
        }
    }

    /// Publish holdings
    pub fn publish(&self) {
        self.holdings.lock().expect("Mb holdings").publish();
    }

    /// Update inputs
    pub fn update(&self) {
        self.inputs.lock().expect("Mb inputs").update();
    }

    /// Bit at address `a`
    pub fn bit(&self, a: usize) -> Result<bool, Error> {
        if 00001 <= a && a < 00001 + self.coil_count {
            let p = a - 00001;
            let i = p / 16;
            let mask = 1 << (p % 16);
            Ok(self.holdings.lock().expect("Mb holdings").input_buffer()[i] & mask == mask)
        } else if 10001 <= a && a < 10001 + self.din_count {
            let p = a - 10001;
            let i = p / 16;
            let mask = 1 << (p % 16);
            Ok(self.inputs.lock().expect("Mb inputs").output_buffer()[i] & mask == mask)
        } else {
            Err(Error::InvalidAddr)
        }
    }

    /// Set bit at address `a`.
    pub fn set_bit(&self, a: usize, flag: bool) -> Result<(), Error> {
        if 00001 <= a && a < 00001 + self.coil_count {
            let p = a - 00001;
            let i = p / 16;
            let mask = 1 << (p % 16);
            if flag {
                self.holdings.lock().expect("Mb holdings").input_buffer()[i] |= mask;
            } else {
                self.holdings.lock().expect("Mb holdings").input_buffer()[i] &= !mask;
            }
            Ok(())
        } else if 10001 <= a && a < 10001 + self.din_count {
            let p = a - 10001;
            let i = p / 16;
            let mask = 1 << (p % 16);
            if flag {
                self.inputs.lock().expect("Mb inputs").output_buffer()[i] |= mask;
            } else {
                self.inputs.lock().expect("Mb inputs").output_buffer()[i] &= !mask;
            }
            Ok(())
        } else {
            Err(Error::InvalidAddr)
        }
    }

    /// I16 value at address `a`
    pub fn i16(&self, a: usize) -> Result<i16, Error> {
        if 30001 <= a && a < 30001 + MB_BLOCK_SIZE {
            Ok(self.inputs.lock().expect("Mb inputs").output_buffer()[a - 30001] as i16)
        } else if 40001 <= a && a < 40001 + MB_BLOCK_SIZE {
            Ok(self.holdings.lock().expect("Mb holdings").input_buffer()[a - 40001] as i16)
        } else {
            Err(Error::InvalidAddr)
        }
    }

    /// Set i16 value at address `a`.
    pub fn set_i16(&self, a: usize, value: i16) -> Result<(), Error> {
        if 30001 <= a && a < 30001 + MB_BLOCK_SIZE {
            self.inputs.lock().expect("Mb inputs").output_buffer()[a - 30001] = value as u16;
            Ok(())
        } else if 40001 <= a && a < 40001 + MB_BLOCK_SIZE {
            self.holdings.lock().expect("Mb holdings").input_buffer()[a - 40001] = value as u16;
            Ok(())
        } else {
            Err(Error::InvalidAddr)
        }
    }

    /// U16 value at address `a`
    pub fn u16(&self, a: usize) -> Result<u16, Error> {
        if 30001 <= a && a < 30001 + MB_BLOCK_SIZE {
            Ok(self.inputs.lock().expect("Mb inputs").output_buffer()[a - 30001] as u16)
        } else if 40001 <= a && a < 40001 + MB_BLOCK_SIZE {
            Ok(self.holdings.lock().expect("Mb holdings").input_buffer()[a - 40001] as u16)
        } else {
            Err(Error::InvalidAddr)
        }
    }

    /// Set u16 value at address `a`.
    pub fn set_u16(&self, a: usize, value: u16) -> Result<(), Error> {
        if 30001 <= a && a < 30001 + MB_BLOCK_SIZE {
            self.inputs.lock().expect("Mb inputs").output_buffer()[a - 30001] = value;
            Ok(())
        } else if 40001 <= a && a < 40001 + MB_BLOCK_SIZE {
            self.holdings.lock().expect("Mb holdings").input_buffer()[a - 40001] = value;
            Ok(())
        } else {
            Err(Error::InvalidAddr)
        }
    }

    /// I32 value at address `a`
    pub fn i32(&self, a: usize) -> Result<i32, Error> {
        if 30001 <= a && a < 30001 + MB_BLOCK_SIZE - 1 {
            let value = unsafe {
                let ptr = &self.inputs.lock().expect("Mb inputs").output_buffer()[a - 30001]
                    as *const u16 as *const i32;
                *ptr
            };
            Ok(value)
        } else if 40001 <= a && a < 40001 + MB_BLOCK_SIZE - 1 {
            let value = unsafe {
                let ptr = &self.holdings.lock().expect("Mb holdings").input_buffer()[a - 40001]
                    as *const u16 as *const i32;
                *ptr
            };
            Ok(value)
        } else {
            Err(Error::InvalidAddr)
        }
    }

    /// Set i16 value at address `a`.
    pub fn set_i32(&self, a: usize, value: i32) -> Result<(), Error> {
        if 30001 <= a && a < 30001 + MB_BLOCK_SIZE - 1 {
            unsafe {
                let ptr = &mut self.inputs.lock().expect("Mb inputs").output_buffer()[a - 30001]
                    as *mut u16 as *mut i32;
                *ptr = value;
            }
            Ok(())
        } else if 40001 <= a && a < 40001 + MB_BLOCK_SIZE - 1 {
            unsafe {
                let ptr = &mut self.holdings.lock().expect("Mb holdings").input_buffer()[a - 40001]
                    as *mut u16 as *mut i32;
                *ptr = value;
            }
            Ok(())
        } else {
            Err(Error::InvalidAddr)
        }
    }

    /// U32 value at address `a`
    pub fn u32(&self, a: usize) -> Result<u32, Error> {
        if 30001 <= a && a < 30001 + MB_BLOCK_SIZE - 1 {
            let value = unsafe {
                let ptr = &self.inputs.lock().expect("Mb inputs").output_buffer()[a - 30001]
                    as *const u16 as *const u32;
                *ptr
            };
            Ok(value)
        } else if 40001 <= a && a < 40001 + MB_BLOCK_SIZE - 1 {
            let value = unsafe {
                let ptr = &self.holdings.lock().expect("Mb holdings").input_buffer()[a - 40001]
                    as *const u16 as *const u32;
                *ptr
            };
            Ok(value)
        } else {
            Err(Error::InvalidAddr)
        }
    }

    /// Set i16 value at address `a`.
    pub fn set_u32(&self, a: usize, value: u32) -> Result<(), Error> {
        if 30001 <= a && a < 30001 + MB_BLOCK_SIZE - 1 {
            unsafe {
                let ptr = &mut self.inputs.lock().expect("Mb inputs").output_buffer()[a - 30001]
                    as *mut u16 as *mut u32;
                *ptr = value;
            }
            Ok(())
        } else if 40001 <= a && a < 40001 + MB_BLOCK_SIZE - 1 {
            unsafe {
                let ptr = &mut self.holdings.lock().expect("Mb holdings").input_buffer()[a - 40001]
                    as *mut u16 as *mut u32;
                *ptr = value;
            }
            Ok(())
        } else {
            Err(Error::InvalidAddr)
        }
    }
}
