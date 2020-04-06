import React, { useState } from 'react';
import styled from 'styled-components';
import Header from './components/header/header';
import InitialScene from './components/scenes/initialScene';
import MainMenuScene from './components/scenes/mainMenuScene';
import WithdrawScene from './components/scenes/withdrawScene';
import VerificationScene from './components/scenes/verificationScene';

const MainBody = styled.div`
  position: relative;
  width: 100vw;
  height: 100vh;
  background-color: lightgrey;
`;
const ContentArea = styled.div`
  position: absolute;
  top: 100px;
  height: calc(100% - 100px);
  width: calc(100%);
  background-color: green;
`;

function App() {
  const [showScene, setShowScene] = useState('initial');

  function getScene() {
    switch (showScene) {
      case 'initial':
        return <InitialScene buttonOnClick={buttonOnClick} />;
      case 'mainmenu':
        return <MainMenuScene buttonOnClick={buttonOnClick} />;
      case 'withdraw':
        return <WithdrawScene buttonOnClick={buttonOnClick} />;
        case 'verification':
          return <VerificationScene buttonOnClick={buttonOnClick} />;
      default:
        return null;
    }
  }

  function buttonOnClick(keyText) {
    return setShowScene(keyText);
  }
  return (
    <MainBody>
      <Header></Header>
      <ContentArea>{getScene()}</ContentArea>
    </MainBody>
  );
}

export default App;
