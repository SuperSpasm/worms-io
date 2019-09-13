const os = require('os');
var assert = require('assert');

const should = require("chai").should();
const socketio_client = require('socket.io-client');

const end_point = 'http://' + os.hostname() + ':8080';
const opts = {
    forceNew: true
};

describe("async test with socket.io", function () {
    this.timeout(10000);

    it('Response should be an object', function (done) {
        setTimeout(function () {
            const socket_client = socketio_client(end_point, opts);

            let aaa = socket_client.emit('game_start', 'ABCDEF');
            console.log(aaa);

            socket_client.on('event response', function (data) {
                data.should.be.an('object');
                socket_client.disconnect();
                done();
            });

            socket_client.on('event response error', function (data) {
                console.error(data);
                socket_client.disconnect();
                done();
            });
        }, 4000);
    });
});