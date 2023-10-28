import React from "react";

import MenuItem from '../Components/MenuItem';

const ScrollMenu = () => {
    return (<React.Fragment>
        <div className='flex-col items-center rounded-lg hidden lg:flex  w-[30%]  bg-gradient-to-b dark:from-[#f576769c] dark:to-[#ffc87cdf] from-[#7A96FF] to-[#B07CFF] drop-shadow-md'>

            <h1 className='hover:scale-110 duration-200 ease-out relative top-40 basis-auto text-top text-2xl 2xl:text-4xl rounded-lg py-2 px-5 dark:bg-[#21262D] bg-white text-white font-varela'><div className='text-transparent text-white bg-clip-text bg-gradient-to-b dark:from-[#f576769c] dark:to-[#ffc87cdf] from-[#7A96FF] to-[#B07CFF]'>Player Sheet</div></h1>
            <ul className='leading-relaxed relative top-60 list-none basis-auto text-xl text-white font-varela text-center'>
                <MenuItem text="Ficha do Jogador" />
                <MenuItem text="Inventário" />
                <MenuItem text="Anotações" />
                <li className='hover:text-white dark:hover:bg-[#21262D]  hover:bg-white rounded-lg hover:scale-125 duration-300 ease-out px-2  relative top-20'> <div className='hover:text-transparent dark:text-zinc-700 text-white hover:bg-clip-text hover:bg-gradient-to-b dark:from-[#f576769c] dark:to-[#ffc87cdf] from-[#7A96FF] to-[#B07CFF]'>Configurações</div>
                </li>
            </ul>
        </div>
        </React.Fragment>
)}

export default ScrollMenu;