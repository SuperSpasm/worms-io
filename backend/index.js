const express = require('express');
const app = express();
const http = require('http').Server(app);
const io = require('socket.io')(http);
const _ = require('lodash');

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
        console.log('add_users', data);
        socket.broadcast.emit('update_users', Users);
    });

    socket.on('explosion', function(data) {
        console.log('change_user_state', data);
        WorldState.push(data);
        socket.broadcast.emit('update_world_state', WorldState);
    });

    socket.on('change_user_state', function(data) {
        console.log('change_user_state', data);
        socket.broadcast.emit('update_user_state', data);
    });

    socket.on('disconnect', function(username) {
        console.log('User disconnected');
    })
});

const server = http.listen(8080, function() {
    console.log('listening on *:8080');
});