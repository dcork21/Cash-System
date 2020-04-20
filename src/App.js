import React, { useState } from 'react';
import styled from 'styled-components';
import Header from './components/header/header';
import InitialScene from './components/scenes/initialScene';
import MainMenuScene from './components/scenes/mainMenuScene';
import WithdrawScene from './components/scenes/withdrawScene';
import VerificationScene from './components/scenes/verificationScene';
import LoginScene from './components/scenes/loginScene';
import RegistrationScene from './components/scenes/registrationScene';
import RegistrationFailed from './components/scenes/registrationFailScene';
import RegistrationSuccess from './components/scenes/registrationSuccessScene';

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
  const [userName, setUserName] = useState('');
  const [sessionToken, setSessionToken] = useState('');
  const [showScene, setShowScene] = useState('login');
  const [amount, setAmount] = useState(10);

  function buttonOnClick(keyText) {
    console.log('keyText', keyText);
    return setShowScene(keyText);
  }

  function setWithdrawAmount(newAmount) {
    if ((amount > 5 && newAmount < 0) || (amount < 250 && newAmount > 0))
      return setAmount(amount + newAmount);
  }

  function loginSuccessFunction(userName, token) {
    setUserName(userName);
    setSessionToken(token);
    return setShowScene('initial');
  }
  function getScene() {
    switch (showScene) {
      case 'login':
        return (
          <LoginScene
            loginSuccessFunction={loginSuccessFunction}
            sessionToken={sessionToken}
            registerFunction={() => setShowScene('register')}
          />
        );
      case 'register':
        return (
          <RegistrationScene
            buttonOnClick={buttonOnClick}
            loginFunction={() => setShowScene('login')}
          />
        );
      case 'registersuccess':
        return <RegistrationSuccess buttonOnClick={buttonOnClick} />;
      case 'registerfail':
        return <RegistrationFailed buttonOnClick={buttonOnClick} />;
      case 'initial':
        return (
          <InitialScene buttonOnClick={buttonOnClick} userName={userName} />
        );
      case 'mainmenu':
        return <MainMenuScene buttonOnClick={buttonOnClick} />;
      case 'withdraw':
        return (
          <WithdrawScene
            amountClick={setWithdrawAmount}
            confirmClick={buttonOnClick}
            amount={amount}
          />
        );
      case 'verification':
        return (
          <VerificationScene buttonOnClick={buttonOnClick} amount={amount} />
        );
      default:
        return null;
    }
  }

  return (
    <MainBody>
      <Header></Header>
      <ContentArea>{getScene()}</ContentArea>
    </MainBody>
  );
}

export default App;
