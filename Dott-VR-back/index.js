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
    console.log("new connection Socket: " + socket.address);
    
    JSON.stringify()
    
    socket.emit('message', {hello: 'world'});
    socket.on('getGames', function(){
        console.log("Games list required by the game!");
        socket.emit('gamesListReceived',   {games:[{name: "Game1"},
                                            {name: "Game2"},
                                            {name: "Game3"}]} );
    });
} );

console.log('--- Server is running ---')