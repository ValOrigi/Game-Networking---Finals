const express = require('express');
const router = express.Router();

const { createPlayer, getAllPlayers, getPlayerByID, login, updateScore, deletePlayerbyID} = require('../controllers/playerController');

router.post('/register', createPlayer);
router.get('/', getAllPlayers);
router.get('/:id', getPlayerByID);
router.post('/login', login);
router.post('/score/:id', updateScore);
router.delete('/delete/:id', deletePlayerbyID);

module.exports = router;