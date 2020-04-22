import React, { useState } from 'react';
import styled from 'styled-components';
import Header from './components/header/header';
import SceneFactory from './components/scenes/sceneFactory';

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
  const [userSession, setUserSession] = useState({ Identity: {} });
  const [withdrawals, setWithdrawals] = useState([]);
  const [showScene, setShowScene] = useState('login');
  const [sceneProps, setSceneProps] = useState({});

  function buttonOnClick(keyText, keyProps) {
    setSceneProps(keyProps);
    return setShowScene(keyText);
  }
  function loginSuccessFunction(session) {
    setUserSession(session);
    return setShowScene('initial');
  }

  function updateWithdrawals(newWithdrawal) {
    setWithdrawals([...withdrawals, newWithdrawal]);
    setSceneProps(newWithdrawal);
    return setShowScene('withdrawverify');
  }
  function registerAccountSuccessFunction(response) {
    let accounts = userSession.accounts || [];
    accounts.push(response);
    setUserSession({ ...userSession, accounts });
    setSceneProps(response);
    return setShowScene('manageaccount');
  }
  return (
    <MainBody>
      <Header></Header>
      <ContentArea>
        <SceneFactory
          {...{ userSession, showScene, sceneProps, withdrawals }}
          functions={{
            buttonOnClick,
            showScene,
            loginSuccessFunction,
            registerAccountSuccessFunction,
            setShowScene,
            updateWithdrawals,
          }}
        />
      </ContentArea>
    </MainBody>
  );
}

export default App;
