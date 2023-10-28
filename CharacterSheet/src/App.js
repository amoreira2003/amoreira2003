import './App.css';

import CharacterInfo from './Components/CharacterInfo.js'
import SkillsMenu from './Components/SkillsMenu';
import CharacterContext from './Context/CharacterContext';

import DiceRollBackground from './Components/DiceRollBackground';

import MobileScrollMenu from './Components/MobileScrollMenu';
import ScrollMenu from './Components/ScrollMenu';

import React, {useState, useEffect} from 'react';

import { useParams } from 'react-router-dom';


function App() {
  const { id } = useParams();
  const [width, setWidth] = useState(window.innerWidth)
  const [character, setCharacter] = useState(id)

  function resize() {
    setWidth(window.innerWidth)
  }  
  
  useEffect(() => {
    window.addEventListener("resize", resize)
    return () =>  {window.removeEventListener("resize",resize);}
  },[window.innerWidth])

  return (<CharacterContext.Provider value={{setCharacter,character}}><React.Fragment>

    <DiceRollBackground />

    <div className='lg:m-20 flex flex-col lg:flex-row flex-shrink h-fit gap-4'>

      {width > 1023 ? <ScrollMenu/> : <MobileScrollMenu/>}

      <div className='flex flex-col flex-shrink basis-full gap-4'>

        <CharacterInfo />

        <SkillsMenu />

      </div>
    </div>

  </React.Fragment>
  </CharacterContext.Provider>
  );
}

export default App;
