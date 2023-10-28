
function rollDice(value, shortName, displayText) {
    let random = Math.floor(Math.random() * 100) + 1;
    let result;
    console.log(random)
    if (random === 1) {
        result = 'Sucesso Critico'
    } else if (random < Math.round(value / 5)) {
        result = 'Sucesso Extremo'
    } else if (random < Math.round(value / 2)) {
        result = 'Sucesso Bom'
    } else if (random <= value) {
        result = 'Sucesso Regular'
    } else if (random > value && random < 96) {
        result = 'Fracasso'
    } else {
        result = 'Desastre'
    }
    document.getElementById("skillType").innerText = displayText;
    document.getElementById("successType").innerText = result + " (" + random + ")";
    document.getElementById("diceBackground").style = "visibility: visible"

    return { result, random }

}

const Stats = ({displayText,value,shortName}) => {

    return (<div><li onClick={() => { rollDice(value,shortName,displayText) }}
        className='hover:scale-110 m-1 px-4 duration-300 ease-out bg-gradient-to-b dark:from-[#f576769c] dark:to-[#ffc87cdf] from-[#7A96FF] to-[#B07CFF] rounded-lg p-1 text-fake-white font-varela  text-center text-lg'>
        {shortName} {value}</li>
    </div>);

}

export default Stats;