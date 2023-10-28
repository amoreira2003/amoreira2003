// Create a react component called Player List with a state variable called players that is an array of objects with the following properties:

import React, { useState, useEffect } from "react";
import { Reorder, motion, AnimatePresence } from "framer-motion";
import anaPicture from "../Images/characters/ana.jfif";
import pauloPicture from "../Images/characters/paulo.jfif";
import sofiaPicture from "../Images/characters/sofia.jfif";
import Sockette from 'sockette';

function PlayerList() {
  const [players, setPlayers] = useState([
    "sofia.json",
    "ana.json",
    "paulo.json",
  ]);

  const [ws, setWs] = useState(null);
  const [color, setColor] = useState('#000000'); 

  const [currentSelected, setCurrentSelected] = useState(0);
  const handleColorChange = () => {
    // Send the color change command to the WebSocket server
    const hexColor = color.replace("#", "");

    // Parse the hexadecimal color code to RGB components
    const red = parseInt(hexColor.slice(0, 2), 16);
    const green = parseInt(hexColor.slice(2, 4), 16);
    const blue = parseInt(hexColor.slice(4, 6), 16);
    
    // Convert RGB values to strings and pad with leading zeros if necessary
    const hexRed = red.toString(10).padStart(3, '0');
    const hexGreen = green.toString(10).padStart(3, '0');
    const hexBlue = blue.toString(10).padStart(3, '0');
    
    console.log(`Sending color: ${hexRed}, ${hexGreen}, ${hexBlue}`)
    // Create the "COLRRRGGGBBB" format string
    const colorString = `COL${hexRed}${hexGreen}${hexBlue}`;
    if (ws) {
      ws.send(colorString);
    }
  };


  useEffect(() => {
    // Replace with your ESP32 WebSocket server address
    const serverAddress = 'ws://192.168.0.104:81';

    // Create a WebSocket connection
    const socket = new Sockette(serverAddress, {
      timeout: 5e3,
      maxAttempts: 10,
      onopen: e => {console.log('Connected:', e);   setWs(socket);},
      onmessage: e => console.log('Received:', e),
      onreconnect: e => console.log('Reconnecting...', e),
      onmaximum: e => console.log('Stop Attempting!', e),
      onclose: e => console.log('Closed!', e),
      onerror: e => console.log('Error:', e),
    });

  

    return () => {
      // Close the WebSocket connection when the component unmounts
      if (socket) {
        socket.close();
      }
    };
  }, []);


  useEffect(() => {

    handleColorChange();

    }, [color])

  useEffect(() => {

      switch (players[currentSelected]) {
        case "sofia.json":
            setColor("#3D24FE");
            break;

        case "ana.json":
            setColor("#E100FF")
            break;
        
        case "paulo.json":
            setColor("#00FF00")
            break;
    
        default:
            break;
    }

    },[changeOrder])

  function changeOrder(newOrder) {
    setPlayers(newOrder);
    console.log(newOrder[currentSelected]);
    switch (newOrder[currentSelected]) {
        case "sofia.json":
            setColor("#3D24FE");
            break;

        case "ana.json":
            setColor("#E100FF")
            break;
        
        case "paulo.json":
            setColor("#00FF00")
            break;
    
        default:
            break;
    }

   
  }

  return (
    <div className="flex flex-row  h-screen w-screen">
  
      <Reorder.Group
        className=" flex flex-row justify-center items-center  gap-4 h-screen w-screen"
        axis="x"
        values={players}
        onReorder={changeOrder}
      >

        
        {players.map((players, index) => {

            var image
          if (players === "ana.json") {
            image = anaPicture;
          } else if (players === "paulo.json") {
            image = pauloPicture;
          } else if (players === "sofia.json") {
            image = sofiaPicture
          }
          return (
            
            <Reorder.Item
              className="w-48 h-48 "
              id={players}
              value={players}
              key={players}
              onClick={() => setCurrentSelected(index)}
            >
            <div onDrag={(e) => e.stopPropagation()}> <img  className={`rounded ${currentSelected == index ? 'shadow-sm border-4  border-green-500' : ''}`} src={image}/></div>
            </Reorder.Item>
          );
        })}
      </Reorder.Group>
 
    </div>
  );
}

export default PlayerList;
