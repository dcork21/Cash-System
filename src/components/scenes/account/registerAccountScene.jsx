import React, { useState } from 'react';
import styled from 'styled-components';
import Button from '../../generic/button';
import FormInput from '../../generic/formInput';
import { registerAccount } from '../../../requests/accountRequests';

const ContentArea = styled.div`
  position: relative;
  width: 100%;
  height: 100%;
  background-color: #9e9e9e;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  justify-content: center;
`;

const RegistrationArea = styled.div`
  position: relative;
  height: auto;
  width: 75%;
  margin-left: auto;
  margin-right: auto;
  border: 1px solid white;
  border-radius: 5px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  padding: 20px;
`;

const RegistrationMessage = styled.div`
  position: relative;
  width: 100%;
  height: 20px;
  margin-left: auto;
  margin-right: auto;
  background-color: #9e9e9e;
  box-sizing: border-box;
  display: flex;
  justify-content: center;
  text-align: center;
  font-size: 20px;
  font-family: ${'Calibri (Body)'};
  font-weight: ${'Bold'};
`;

const WarningMessage = styled.div`
  position: relative;
  color: red;
  width: 100%;
  height: 15px;
  margin-left: auto;
  margin-right: auto;
  box-sizing: border-box;
  display: flex;
  justify-content: center;
  text-align: center;
  font-size: 16px;
  font-family: ${'Calibri (Body)'};
`;
const RegistrationPositon = styled.div`
  position: relative;
  width: 100%;
  height: auto;
  margin-right: auto;
  padding-bottom: 50px;
`;

const ButtonPositon = styled.div`
  position: absolute;
  width: 30%;
  bottom: 5px;
  right: 5px;
`;

export default function RegisterAccountScene(props) {
  const { registerAccountSuccessFunction, userSession } = props;
  const [hasFailed, setHasFailed] = useState(false);
  const [warnings, setWarnings] = useState([]);
  const [accountNumber, setAccountNumber] = useState('');
  const [sortCode, setSortCode] = useState('');
  const [balance, setBalance] = useState(0);

  async function doRegister() {
    let newWarnings = [];

    if (accountNumber === '') newWarnings.push('Account Number');
    if (sortCode === '') newWarnings.push('Sort Code');
    if (newWarnings.length > 0) return setWarnings(newWarnings);

    setWarnings(newWarnings);
    let response = await registerAccount(
      userSession.sessionToken,
      userSession.userId,
      accountNumber,
      sortCode,
      balance
    );
    if (!response) return setHasFailed(true);
    return registerAccountSuccessFunction(response);
  }

  function updateSortCode(value) {
    setSortCode(value.replace(/(\d{2})(\d{2})(\d{2})/, "$1-$2-$3"));
  }
  return (
    <ContentArea>
      <RegistrationArea>
        <RegistrationMessage>Register New Account</RegistrationMessage>
        <WarningMessage>
          {hasFailed
            ? 'Account creation failed'
            : warnings.length > 0 && 'Please check the marked fields'}
        </WarningMessage>
        <RegistrationPositon>
          <FormInput
            showWarning={warnings.includes('Account Number')}
            inputKey={'Account Number'}
            inputValue={accountNumber}
            updateFunction={setAccountNumber}
          />
          <FormInput
            showWarning={warnings.includes('Sort Code')}
            inputKey={'Sort Code'}
            maxLength={8}
            inputValue={sortCode}
            updateFunction={updateSortCode}
          />
          <FormInput
            showWarning={warnings.includes('Balance')}
            inputKey={'Balance'}
            inputValue={balance}
            updateFunction={setBalance}
          />
          <ButtonPositon>
            <Button text={'Submit'} onClickFunc={() => doRegister()}></Button>
          </ButtonPositon>
        </RegistrationPositon>
      </RegistrationArea>
    </ContentArea>
  );
}
