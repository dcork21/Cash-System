import React, { useState, useEffect } from 'react';
import styled from 'styled-components';
import Header from './components/header/header';
import InitialScene from './components/scenes/initialScene';
import MainMenuScene from './components/scenes/mainMenuScene';
import WithdrawScene from './components/scenes/withdrawScene';
import VerificationScene from './components/scenes/verificationScene';
import LoginScene from './components/scenes/loginScene';

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
  const [loginToken, setLoginToken] = useState('');
  const [showScene, setShowScene] = useState('login');
  const [amount, setAmount] = useState(10);

  async function loginClick(userName, password) {
    const LoginRequest = {
      method: 'POST',
      mode: 'cors',
      cache: 'no-cache',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        UserName: userName,
        Password: password,
        LoginToken: loginToken,
      }),
    };
    const response = await fetch(
      'https://localhost:44356/Login/Post',
      LoginRequest
    ).then((res) => res.json());
    setUserName(response.userName);
    return setLoginToken(response.token);
  }

  function buttonOnClick(keyText) {
    return setShowScene(keyText);
  }

  function setWithdrawAmount(newAmount) {
    if ((amount > 5 && newAmount < 0) || (amount < 250 && newAmount > 0))
      return setAmount(amount + newAmount);
  }

  useEffect(() => {
    if (loginToken !== 'Unauthorized' && loginToken !== '') {
      setShowScene('initial');
    }
    return () => {};
  }, [loginToken]);

  function getScene() {
    switch (showScene) {
      case 'login':
        return (
          <LoginScene buttonOnClick={loginClick} loginToken={loginToken} />
        );
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
