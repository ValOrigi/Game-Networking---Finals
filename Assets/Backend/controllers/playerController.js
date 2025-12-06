const { json } = require("express");
const Player = require("../models/Player");

exports.createPlayer = async (req, res) => {
  try {
      const player = await Player.create(req.body);

      res.status(201).json({
        success: true,
        message: "Player created successfully",
        data: player
      })
  } catch (error){

    if (error.name == 'validationError')
    {
      const messages = Object.values(error.errors).map(err => err.message);
      return res.status(400).json({
        success: false,
        message: "Validation error",
        errors: messages
      })
    }
    
    if(error.code === 11000){
        const field = Object.keys(error.keyPattern)[0];
        return res.status(400),json({
          success: false,
          message: `${field} already exists`
        });
    }
  }
}

exports.getAllPlayers = async (req, res) => {
    try {
      const player = await Player.find();
      res.status(200).json({
        success: true,
        count: player.length,
        data: player

      })
    }catch (error){
      res.status(500).json({
        success: false,
        error: error.message

      })
    }
}

exports.getPlayerByID = async (req, res) => {
    try {
      const id = req.params.id;
      const player = await Player.findById(id);

      if(!player)
      {
        return res.status(404).json({
          success: false,
          message: "Player not found"

        })
      }

      res.status(200).json({
        success: true,
        data: player

      })
    }catch (error){
      res.status(500).json({
        success: false,
        error: error.message

      })
    }
}

exports.login = async (req, res) => {
   try {
      const { username, password } = req.body;

      if(!username || !password)
        {
        return res.status(400).json({
          success: false,
          message: "please provide a valid username or password"
        })
        }

      const player = await Player.findOne ({username});
      if (!player || player.password !== password)
      {
        res.status(400).json({
          success: false,
          message: "Invaid username or password"
        })
      }

      res.status(200).json({
        success: true,
        message: "Login Successful",
        data: player

      })

    }catch (error){
      res.status(500).json({
        success: false,
        error: error.message

      })
    }
}

exports.updateScore = async (req, res) => {
   try {
      const id = req.params.id
      const {score} = req.body;

      const player = await Player.findById(id);

      if(!player)
      {
        return res.status(404).json({
          success: false,
          message: "Player not found"

        })
      }

      player.score = score;
      if(score < 0) player.score = 0;

      await player.save();
      
      res.status(200).json({
        success: true,
        data: player

      })
    }catch (error){
      res.status(500).json({
        success: false,
        error: error.message

      })
    }
}

exports.deletePlayerbyID = async (req, res) => {
  try {
    const id = req.params.id;
    const player = await Player.findById(id);

    if (!player) {
      return res.status(404).json({
        success: false,
        message: "Player not found",
      });
    }

    await Player.findByIdAndDelete(id);

    res.status(200).json({
      success: true,
      message: `Player with ID ${id} has been deleted`,
    });
  } catch (error) {
    res.status(500).json({
      success: false,
      error: error.message,
    });
  }
};

