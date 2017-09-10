extern crate botnanars;
extern crate libc;

use libc::c_char;
use std::ffi::CStr;
use std::str;

#[no_mangle]
pub extern "C" fn start(connection: *const c_char) {
    let c_connection = unsafe {
        assert!(!connection.is_null());

        CStr::from_ptr(connection)
    };

    let r_connection = c_connection.to_str().unwrap();
    botnanars::start(r_connection);
}

#[cfg(test)]
mod tests {
    #[test]
    fn it_works() {
        assert_eq!(2 + 2, 4);
    }
}
