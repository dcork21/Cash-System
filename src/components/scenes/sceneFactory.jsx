import React from 'react';
import InitialScene from './initialScene';
import MainMenuScene from './mainMenuScene';
import WithdrawScene from './withdrawal/withdrawScene';
import VerificationScene from './withdrawal/withdrawalVerifyScene';

import LoginScene from './user/loginScene';
import RegistrationScene from './user/registrationScene';
import RegistrationFailed from './user/registrationFailScene';
import RegistrationSuccess from './user/registrationSuccessScene';
import AccountMenuScene from './account/accountMenuScene';
import RegisterAccountScene from './account/registerAccountScene';
import AccountScene from './account/accountScene';
import WithdrawalMenuScene from './withdrawal/withdrawalMenuScene';

export default function SceneFactory(props) {
  const { functions, showScene, userSession, sceneProps, withdrawals } = props;
  const {
    buttonOnClick,
    loginSuccessFunction,
    registerAccountSuccessFunction,
    setShowScene,
    updateWithdrawals,
  } = functions;

  switch (showScene) {
    case 'mainmenu':
      return (
        <MainMenuScene
          key={withdrawals.length}
          buttonOnClick={buttonOnClick}
          showWithdrawalMenu={withdrawals && withdrawals[0]}
        />
      );
    // User
    case 'login':
      return (
        <LoginScene
          loginSuccessFunction={loginSuccessFunction}
          sessionToken={userSession.sessionToken}
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

    // Account
    case 'accountmenu':
      return (
        <AccountMenuScene
          accounts={userSession.accounts}
          buttonOnClick={buttonOnClick}
        />
      );

    case 'manageaccount':
      return <AccountScene {...sceneProps} buttonOnClick={buttonOnClick} />;

    case 'registeraccount':
      return (
        <RegisterAccountScene
          registerAccountSuccessFunction={registerAccountSuccessFunction}
          userSession={userSession}
        />
      );

    // Withdrawal
    case 'withdraw':
      return (
        <WithdrawScene
          buttonOnClick={buttonOnClick}
          {...{ ...sceneProps, ...userSession, updateWithdrawals }}
        />
      );

    case 'withdrawmenu':
      return (
        <WithdrawalMenuScene
          buttonOnClick={buttonOnClick}
          withdrawals={withdrawals}
        />
      );

    case 'withdrawverify':
      return (
        <VerificationScene buttonOnClick={buttonOnClick} {...sceneProps} />
      );
    case 'initial':
    default:
      return (
        <InitialScene
          userName={userSession.userName}
          buttonOnClick={buttonOnClick}
        />
      );
  }
}
