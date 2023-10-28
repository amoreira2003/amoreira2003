
import React from 'react'
import dice from '../Images/dice.png'


function closeBackground() {
    document.getElementById('diceBackground').style = "visibility: invisible"
  }

const DiceRollBackground = () => {
    return(<React.Fragment>
    <div onClick={() => closeBackground()} id='diceBackground' className='fixed invisible top-0 z-10 bg-opacity-40 m-0 w-full h-full bg-black'>
      <div className='flex flex-col h-full w-full  place-items-center self-center justify-content-center align-center'>
      <h1 id='skillType'className='text-xl lg:text-4xl text-white font-varela mt-auto'>SkillName</h1>
        <img src={dice} className='animate-spin-slow w-24 lg:w-56' alt='dice'></img>
        <h1 id='successType'className='text-xl lg:text-4xl text-white font-varela mb-auto'>Sucess Type (Number)</h1>
      </div>
    </div>
    </React.Fragment>
)}

export default DiceRollBackground;