const express = require('express');
const app = express();
const http = require('http').Server(app);
const io = require('socket.io')(http);
const _ = require('lodash');
const usersEvents = require('./events/Users');

app.get('/', function(req, res) {
    res.send('Hello world!');
});

const Users = [];
const WorldState = [];

io.sockets.on('connection', function(socket) {
    console.log('New user connected.');
    io.emit({
        WorldState,
        Users
    });


    socket.on('add_users', function(data) {
        Users.push({
            id: uuid(),
            ...msg
        })
        socket.broadcast.emit(Users);
    });

    socket.on('world_state', function(data) {
        WorldState.push(data);
        socket.broadcast.emit(WorldState);
    });

    socket.on('explosion', function(data) {
        socket.broadcast.emit(data);
    });

    socket.on('user_state', function(data) {
        socket.broadcast.emit(data);
    });

    socket.on('disconnect', function(username) {
        console.log('User disconnected');
    })

});


const server = http.listen(8080, function() {
    console.log('listening on *:8080');
});