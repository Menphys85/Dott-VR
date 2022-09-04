var app = require('express')();
var server = require('http').createServer(app);
var io = require('socket.io')(server);

server.listen(3000);
var gameObjects =[];
var playerSpawnpoints = [];
var clients = [];



app.get('/', function(req, res){
    res.send('Hello, you have got a back get "/" ')
});

io.on('connection', function(socket){
    
    var currenPlayer ={};
    currenPlayer.name = 'unknow';
    
    socket.on('player connect', function (){
        console.log(currenPlayer.name + 'recv: player connect');
        for (var i=0; i<clients.length; i++){
            var playerConnected = {
                name:clients[i].name,
                position: clients[i].position,
                rotation: clients[i].rotation,
                health:clients[i].health
            };

            socket.emit('other player connected', playerConnected);
            console.log(currenPlayer.name +' emit: other player connected: '+ JSON.stringify(playerConnected))
            
        }
        
    });
    
    socket.emit('message', {hello: 'world'});
    socket.on('mesage', function(data){
        console.log(data);
    });
} );

console.log('--- Server is running ---')