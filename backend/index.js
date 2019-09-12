const express = require('express');
const app = express();
const http = require('http').Server(app);
const io = require('socket.io')(http);
const _ = require('lodash');
const usersEvents = require('./events/Users');

app.get('/', function(req, res) {
    res.send('Hello world!');
});

const WorldState = {};
const UserState = {};
const Explosion = {};

io.sockets.on('connection', function(socket) {
    console.log('New user connected.');

    socket.on('users', function(data) {
        users = usersEvents.do(data);
        socket.broadcast.emit(users);
    });

    socket.on('disconnect', function(username) {
        console.log('User disconnected');
    })

    socket.on('chat_message', function(message) {
        io.emit('chat_message', '<strong>' + socket.username + '</strong>: ' + message);
    });

});


const server = http.listen(8080, function() {
    console.log('listening on *:8080');
});