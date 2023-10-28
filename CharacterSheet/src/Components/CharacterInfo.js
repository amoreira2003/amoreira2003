import React, { useState, useEffect, useContext } from "react";
import anaPicture from "../Images/characters/ana.jfif";
import pauloPicture from "../Images/characters/paulo.jfif";
import sofiaPicture from "../Images/characters/sofia.jfif";
import Stats from "../Components/Stats.js";
import axios from "axios";
import CharacterContext from "../Context/CharacterContext";

const CharacterInfo = () => {
  const [images, setImage] = useState(null)
  const [info, setInfo] = useState(null);
  const [uid, setUid] = useState("");
  const { setCharacter, character } = useContext(CharacterContext);
  var image;
  const fetchCharacterInformation = async () => {
    const response = await axios.get(
      `http://localhost:5000/characters/${character}`
    );

    // by character name set the character image
    if (character === "ana.json") {
      setImage(anaPicture);
    } else if (character === "paulo.json") {
      setImage(pauloPicture);
    } else if (character === "sofia.json") {
      setImage(sofiaPicture);
    }

    console.log(response);
    setInfo(response.data);
  };

  useEffect(() => {
    fetchCharacterInformation();
  }, []);

  if (!info) return <div>Loading</div>;

  return (
    <div className="flex flex-col align-center rounded-lg dark:bg-[#21262D] bg-white drop-shadow-md">
      <div className="flex flex-row gap-1 p-1 align-center justify-center text-white flex-wrap ">
        <img
          src={images}
          alt="CharacterPfp"
          className="m-auto lg:m-8 rounded-lg  bg-gradient-to-b dark:from-[#f576769c] dark:to-[#ffc87cdf] from-[#7A96FF] to-[#B07CFF] w-28 h-28 sm:w-48 sm:h-48 md:w-64 md:h-64"
        ></img>

        <div className="m-8 flex flex-row lg:flex-col flex-wrap justify-center align-center place-items-center basis-32">
          <React.Fragment>
            <div>
              <h1 className="font-varela dark:text-white  dark:placeholder-gray-400 text-black text-center text-2xl">
                {info.displayName}
              </h1>
              <h1 className="font-varela dark:text-zinc-200 text-zinc-500 text-center text-md">
                {info.origin}
              </h1>
              <br />
            </div>

            <div className="flex flex-row gap-1 w-full">
              <div className="group hover:scale-110 hover:h-5 duration-300 ease-out hover:mx-2 w-1/2 h-3 self-start dark:bg-gray-600  bg-gray-200 rounded-full">
                {" "}
                <div
                  style={{
                    width: `${
                      (info.status.currentHealth / info.status.maxHealth) * 100
                    }%`,
                  }}
                  className={
                    "bg-red-600 dark:bg-[#ef44069c] group-hover:visible group-hover:h-5 duration-300 ease-out h-3 font-varela rounded-lg"
                  }
                >
                  <h1 className="text-center justify-center group-hover:visible invisible">
                    {info.status.currentHealth} / {info.status.maxHealth}
                  </h1>
                </div>{" "}
              </div>
              <div className="group hover:scale-110 hover:h-5 duration-300 ease-out hover:mx-2 w-1/2 h-3 self-start dark:bg-gray-600  bg-gray-200 rounded-full">
                {" "}
                <div
                  style={{
                    width: `${
                      (info.status.currentFadigue / info.status.maxFadigue) *
                      100
                    }%`,
                  }}
                  className={
                    "bg-blue-600 dark:bg-[#2564eb9a group-hover:visible group-hover:h-5 duration-300 ease-out h-3 font-varela rounded-lg"
                  }
                >
                  <h1 className="text-center justify-center group-hover:visible invisible">
                    {info.status.currentFadigue} / {info.status.maxFadigue}
                  </h1>
                </div>{" "}
              </div>
            </div>
            <div className="flex justify-center mt-8 flex-row">
              {Object.keys(info.attributes).map((key) => {
                const attribute = info.attributes[key];
                console.log(attribute);
                return (
                  <ul>
                    <Stats
                      displayText={attribute.displayName}
                      shortName={attribute.shortName}
                      value={attribute.value}
                    />
                  </ul>
                );
              })}
            </div>
          </React.Fragment>
        </div>
      </div>
    </div>
  );
};

export default CharacterInfo;
