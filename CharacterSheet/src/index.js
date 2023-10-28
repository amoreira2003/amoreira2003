import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import PlayerList from './Components/PlayerList';
import Card from './Components/Card';
import CardSkill from './Components/CardSkill';
const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <Router>
    <Routes>
      <Route path='/' element={<PlayerList/>}/>   
      <Route path="/cards" element={<div className='h-screen w-screen flex justify-center items-center'><Card/><CardSkill/></div>}/>
    <Route path="/characters/:id" element={<App />} />
    </Routes>
    </Router>);