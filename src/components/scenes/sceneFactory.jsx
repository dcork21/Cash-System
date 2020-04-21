import React from 'react';
import InitialScene from './initialScene';
import MainMenuScene from './mainMenuScene';
import WithdrawScene from './withdrawal/withdrawScene';
import VerificationScene from './withdrawal/verificationScene';

import LoginScene from './user/loginScene';
import RegistrationScene from './user/registrationScene';
import RegistrationFailed from './user/registrationFailScene';
import RegistrationSuccess from './user/registrationSuccessScene';
import AccountMenuScene from './account/accountMenuScene';
import RegisterAccountScene from './account/registerAccountScene';
import AccountScene from './account/accountScene';

export default function SceneFactory(props) {
  const { functions, showScene, userSession, sceneProps } = props;
  const {
    buttonOnClick,
    loginSuccessFunction,
    registerAccountSuccessFunction,
    setShowScene,
  } = functions;
  const scenes = {
    // Misc
    initial: (
      <InitialScene
        userName={userSession.userName}
        buttonOnClick={buttonOnClick}
      />
    ),
    mainmenu: <MainMenuScene buttonOnClick={buttonOnClick} />,

    // User
    login: (
      <LoginScene
        loginSuccessFunction={loginSuccessFunction}
        sessionToken={userSession.sessionToken}
        registerFunction={() => setShowScene('register')}
      />
    ),
    register: (
      <RegistrationScene
        buttonOnClick={buttonOnClick}
        loginFunction={() => setShowScene('login')}
      />
    ),
    registersuccess: <RegistrationSuccess buttonOnClick={buttonOnClick} />,
    registerfail: <RegistrationFailed buttonOnClick={buttonOnClick} />,

    // Account
    accountmenu: (
      <AccountMenuScene
        accounts={userSession.accounts}
        buttonOnClick={buttonOnClick}
      />
    ),
    manageaccount: (
      <AccountScene {...sceneProps} buttonOnClick={buttonOnClick} />
    ),
    registeraccount: (
      <RegisterAccountScene
        registerAccountSuccessFunction={registerAccountSuccessFunction}
        userSession={userSession}
      />
    ),

    // Withdrawal
    withdraw: <WithdrawScene confirmClick={buttonOnClick} {...sceneProps} />,
    withdrawconfirmation: <VerificationScene buttonOnClick={buttonOnClick} />,
    withdrawfail: null,
    withdrawstatus: null,
  };
  return scenes[showScene];
}
