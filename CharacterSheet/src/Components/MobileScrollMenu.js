import React from "react";


const MobileScrollMenu = () => {
    return (<div className="bg-gradient-to-b flex justify-around dark:from-[#f576769c] dark:to-[#ffc87cdf] from-[#7A96FF] to-[#B07CFF] drop-shadow-md rounded-b-md">
        <div className="hidden sm:block dark:bg-[#21262D] bg-white h-auto m-2 py-4 px-2 rounded-lg"><h1 className="text-transparent font-bold text-white bg-clip-text text-lg md:text-2xl bg-gradient-to-b dark:from-[#f576769c] dark:to-[#ffc87cdf] from-[#7A96FF] to-[#B07CFF]">Character Sheet</h1></div>
        <div className="dark:bg-[#21262D] bg-white h-auto my-1 mx-[2px] py-4 px-2 rounded-lg"><a href="" className="dark:font-bold text-sm dark:text-white md:text-lg">Ficha do Jogador</a></div>
        <div className="dark:bg-[#21262D] bg-white h-auto my-1 mx-[2px] py-4 px-2 rounded-lg"><a href="" className="dark:font-bold dark:text-white text-sm md:text-lg">Inventário</a></div>
        <div className="dark:bg-[#21262D] bg-white h-auto my-1 mx-[2px] py-4 px-2 rounded-lg"><a href="" className="dark:font-bold dark:text-white text-sm  md:text-lg">Anotações</a></div>
        <div className="dark:bg-[#21262D] bg-white h-auto my-1 mx-[2px] py-4 px-2 rounded-lg"><a href="" className="dark:font-bold dark:text-white text-sm  md:text-lg">Configurações</a></div>
    </div>);
}


export default MobileScrollMenu;