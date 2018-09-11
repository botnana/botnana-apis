/*
 * How to use 
 * 
 *  node forth.js WEBSOCKETADDRESS 
 * 
 *  example:
 *      node forth.js 192.168.7.2:3012
 * 
 */
const readline = require('readline')
const websocket = require('ws')

let address
process.argv.forEach((v, index) => {
    if (index === 2) {
        address = v
    }
})

let ws
let interval

if (address) {
    ws = new websocket('ws://' + address)

    ws.on("message", function (data, flags) {
        if (data) {
            // if (botnana.debug_level > 0) {
            //     console.log(data);
            // }
            console.log(data)
        }
    })

    ws.on("open", function () {
        interval = setInterval(poll, 100)
        start()
    })

    ws.on('error', function () {
        console.error('can not connect to ws://' + address)
    })

} else {
    console.error('no websocket address')
    console.error('Example: node forth.js 192.168.7.2:3012')
    return
}

function poll() {
    var json = {
        jsonrpc: "2.0",
        method: "motion.poll"
    };
    ws.send(JSON.stringify(json))
};

function start(address) {
    console.log('To exit , press .exit')
    const rl = readline.createInterface({
        input: process.stdin,
        output: process.stdout
    })

    rl.on('line', (input) => {
        if (input.match('.exit')) {
            rl.close()
            ws.close()
            clearInterval(interval)
            // botnana.close()
            return;
        }
        // console.log(input)
        // botnana.motion.evaluate(input)

        evaluate(input)
    })

    function evaluate(script) {
        var json = {
            jsonrpc: "2.0",
            method: "script.evaluate",
            params: {
                script: script
            }
        };
        ws.send(JSON.stringify(json))
        
    };

}

// botnana.start('ws://localhost:3012')