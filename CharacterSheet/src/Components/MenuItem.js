
const MenuItem = params => {

    return (<div><li className='hover:text-white hover:m-1 dark:hover:bg-[#21262D]  hover:bg-white rounded-lg hover:scale-125 duration-300 ease-out px-2'>
        <div className=' hover:text-transparent dark:text-zinc-700 text-white hover:bg-clip-text hover:bg-gradient-to-b dark:from-[#f576769c] dark:to-[#ffc87cdf] from-[#7A96FF] to-[#B07CFF]'>{params.text}</div>
        </li>
    </div>);

}

export default MenuItem;