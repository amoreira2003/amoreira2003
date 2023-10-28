import React, { useState, useEffect } from "react";
import Skills from "../Components/Skills.js";
import axios from "axios";

const SkillsMenu = () => {
  const [info, setInfo] = useState(null);
  const [uid, setUid] = useState("");

  const fetchCharacterInformation = async () => {
    const response = await axios.get(
      `http://localhost:5000/characters/sofia.json/attributes`
    );
    console.log(response);
    setInfo(response.data);
  };

  useEffect(() => {
    fetchCharacterInformation();
  }, []);

  return (
    <React.Fragment>
      <div className="flex flex-row flex-wrap flex-shrink md:self-center lg:self-start justify-center md:justify-center lg:justify-start gap-1 p-8 rounded-lg dark:bg-[#21262D] bg-white drop-shadow-md">
        <React.Fragment>
          {info &&
            Object.keys(info).map((key) => {
              const attribute = info[key];
              console.log(attribute);
              return (
                attribute &&
                Object.keys(attribute.skills).map((key) => {
                  const skill = attribute.skills[key];

                  return (
                    <Skills
                      displayName={skill.displayName}
                      bonus={skill.bonus}
                      dices={attribute.value}
                    />
                  );
                })
              );
            })}
        </React.Fragment>
      </div>
    </React.Fragment>
  );
};

export default SkillsMenu;
