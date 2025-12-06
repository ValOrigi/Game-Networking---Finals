const express = require('express')
const mongoose = require('mongoose');
const dotenv = require('dotenv');
const  playerRoutes = require('./routes/playerRoutes');

dotenv.config();

mongoose.connect(process.env.MONGODB_URI)
.then(() => console.log("Successfully connected to Database"))
.catch((err)=> console.error(`MongoDB error ${err}`));

const app = express();
app.use(express.json());

app.use('/api/players', playerRoutes);

const PORT = process.env.PORT || 5000;
app.listen(PORT, ()=> {
  console.log('Server is runnong on port 5000');
})


//app.get('/', (req, res)=>{
//   res.json({message: "Welcome to server"});
// })

// app.get('/api/players', (req, res)=>{
//   res.json({message: 'GET: retreive all players'});
// })

// app.get('/api/players/:id', (req, res)=>{
//   res.json({message: `GET: retreive players with ID ${req.params.id}`});
// })

// app.post('/api/players', (req,res)=>{
//   const newPlayer = req.body;

//   res.json({message: 'POST: create new player', data: newPlayer})
// })

// app.put('/api/players/:id', (req, res)=>{
//   const id = req.params.id;
//   const updatedData = req.body;
//   res.json({message: `PUT: Update player ${id}`, data: updatedData})
// })

// app.delete('/api/players/:id', (req, res)=> {
//   const id = req.params.id;
//   es.json({message: `DELETE: Remove player ${id}`})
// })