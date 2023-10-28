var express = require("express");
var cors = require("cors");
var app = express();

app.use(express.json());
app.use(cors());

const port = 5000;

app.get("/", (req, res) => res.status(200).json({ message: "api-working" }));

const getCharacters = () => {
  const fs = require("fs");
  const path = require("path");
  let characters = [];
  const files = fs.readdirSync(path.join(__dirname, "./characters"));
  files.forEach((file) => {
    characters.push(file);
  });

  return characters;
};

app.get("/characters/:uid?", (req, res) => {
  const uid = req.params.uid;

  if (!uid) {
    res.status(200).json(getCharacters());
    return;
  }

  const character = require(`./characters/${uid}`);
  res.status(200).json(character);
});

app.get("/characters/:uid/attributes", (req, res) => {
  const uid = req.params.uid;
  const character = require(`./characters/${uid}`);
  const characterAttributes = character.attributes;
  res.status(200).json(characterAttributes);
});


app.get("/characters/:uid/status", (req, res) => {
  const uid = req.params.uid;
  const character = require(`./characters/${uid}`);
  const characterStatus = character.status;
  res.status(200).json(characterStatus);
});

app.listen(port, () => {
  console.log(`Server running on port ${port}`);
});
