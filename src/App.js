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

  const [showScene, setShowScene] = useState('login');
  const [sceneProps, setSceneProps] = useState('login');

  function buttonOnClick(keyText, keyProps) {
    setSceneProps(keyProps);
    return setShowScene(keyText);
  }

  function loginSuccessFunction(session) {
    console.log('session', session);
    setUserSession(session);
    return setShowScene('initial');
  }

  function registerAccountSuccessFunction(response) {
    let accounts = userSession.accounts || [];
    accounts.push(response);
    setUserSession({ ...userSession, accounts });
  }
  return (
    <MainBody>
      <Header></Header>
      <ContentArea>
        <SceneFactory
          {...{ userSession, showScene, sceneProps }}
          functions={{
            buttonOnClick,
            showScene,
            loginSuccessFunction,
            registerAccountSuccessFunction,
          }}
        />
      </ContentArea>
    </MainBody>
  );
}

export default App;
