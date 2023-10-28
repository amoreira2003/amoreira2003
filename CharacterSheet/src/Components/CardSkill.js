import React, { useEffect } from 'react';
import './Card.css';

const CardSkill = () => {

    useEffect(() => {
        const calculateAngle = (e, item, parent) => {
            let dropShadowColor = `rgba(0, 0, 0, 0.3`;
            if (parent.getAttribute('data-filter-color') !== null) {
                dropShadowColor = parent.getAttribute('data-filter-color');
            }

            parent.classList.add('animated');
            let x = Math.abs(item.getBoundingClientRect().x - e.clientX);
            let y = Math.abs(item.getBoundingClientRect().y - e.clientY);
            let halfWidth = item.getBoundingClientRect().width / 2;
            let halfHeight = item.getBoundingClientRect().height / 2;
            let calcAngleX = (x - halfWidth) / 6;
            let calcAngleY = (y - halfHeight) / 14;
            let gX = (1 - (x / (halfWidth * 2))) * 100;
            let gY = (1 - (y / (halfHeight * 2))) * 100;
            item.querySelector('.glare').style.background = `radial-gradient(circle at ${gX}% ${gY}%, rgb(199 198 243), transparent)`;
            parent.style.perspective = `${halfWidth * 6}px`;
            item.style.perspective = `${halfWidth * 6}px`;
            item.style.transform = `rotateY(${calcAngleX}deg) rotateX(${-calcAngleY}deg) scale(1.04)`;
            parent.querySelector('.inner-card-backface').style.transform = `rotateY(${calcAngleX}deg) rotateX(${-calcAngleY}deg) scale(1.04) translateZ(-4px)`;

            if (parent.getAttribute('data-custom-perspective') !== null) {
                parent.style.perspective = `${parent.getAttribute('data-custom-perspective')}`;
            }

            let calcShadowX = (x - halfWidth) / 3;
            let calcShadowY = (y - halfHeight) / 6;
            item.style.filter = `drop-shadow(${-calcShadowX}px ${-calcShadowY}px 15px ${dropShadowColor})`;
        };

        const cards = document.querySelectorAll('.card');
        cards.forEach((item) => {
            if (item.querySelector('.flip') !== null) {
                item.querySelector('.flip').addEventListener('click', () => {
                    item.classList.add('flipped');
                });
            }
            if (item.querySelector('.unflip') !== null) {
                item.querySelector('.unflip').addEventListener('click', () => {
                    item.classList.remove('flipped');
                });
            }
            item.addEventListener('mouseenter', (e) => {
                calculateAngle(e, item.querySelector('.inner-card'), item);
            });

            item.addEventListener('mousemove', (e) => {
                calculateAngle(e, item.querySelector('.inner-card'), item);
            });

            item.addEventListener('mouseleave', () => {
                let dropShadowColor = `rgba(0, 0, 0, 0.3)`;
                if (item.getAttribute('data-filter-color') !== null) {
                    dropShadowColor = item.getAttribute('data-filter-color');
                }
                item.classList.remove('animated');
                item.querySelector('.inner-card').style.transform = `rotateY(0deg) rotateX(0deg) scale(1)`;
                item.querySelector('.inner-card-backface').style.transform = `rotateY(0deg) rotateX(0deg) scale(1.01) translateZ(-4px)`;
                item.querySelector('.inner-card').style.filter = `drop-shadow(0 10px 15px ${dropShadowColor})`;
            });
        });
    }, []); // Empty dependency array to run the effect once

    return (
        <div className="card skill">
            <span className="inner-card-backface">
                <span className="image">
                    <span className="unflip">Unflip</span>
                </span>
            </span>
            <span className="inner-card">
                <span className="flip">Flip</span>
                <span className="glare"></span>
            </span>
        </div>
    );
};

export default CardSkill;
