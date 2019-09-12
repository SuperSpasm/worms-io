const uuid = require('uuid/v1');

const Users = {};

function do(msg) {
    switch (msg.type) {
        case 'create_new':
            Users.push({
                id: uuid(),
                ...msg
            })
            break;
    }

    return Users;
}

module.exports = {
    do(msg)
};