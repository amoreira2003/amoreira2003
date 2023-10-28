import dice from '../Images/dice.png'

function rollDice(value, skill, dices) {
    let random = [];
    let result;
    for (let i = 0; i < dices; i++) {
        random.push(Math.floor(Math.random() * 20) + 1);
    }
    result = random.join(", ");
    

    document.getElementById("skillType").innerText = skill;
    document.getElementById("successType").innerText = result;
    document.getElementById("diceBackground").style = "visibility: visible"

    return {result,random}

}

const Skills = ({displayName, bonus,dices}) => {
    return (
        <div onClick={() => rollDice(bonus,displayName,dices)} className='group flex flex-col hover:scale-105 m-1 px-4 duration-300 ease-out align-items-center min-w-[40%] max-w-[40%] md:min-w-[20%] md:max-w-[20%] lg:min-w-0 lg:max-w-none h-auto p-2 rounded-lg bg-gradient-to-b dark:from-[#f576769c] dark:to-[#ffc87cdf] from-[#7A96FF] to-[#B07CFF] '>
            <img src={dice} className='w-6 lg:w-8 group-hover:animate-spin-slow ease-out self-center' alt='dice'/>
            <h4 className='text-white text-lg lg:text-sm text-center'>{displayName} <br/></h4> <h4 className='text-white text-lg lg:text-xs text-center'>{bonus}</h4>
        </div>);

}

export default Skills;