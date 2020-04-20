import React, { useState } from 'react';
import styled from 'styled-components';
import Button from '../generic/button';
import FormInput from '../generic/formInput';
import { registerUser } from '../../requests/userRequests';

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
  width: 60%;
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
  height: 50px;
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
  height: 20px;
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
  width: 100%;
  height: auto;
  margin-left: -10px;
  margin-right: auto;
  padding-bottom: 50px;
`;

const LoginPosition = styled.div`
  position: absolute;
  width: 50%;
  height: 20px;
  left: 10px;
  bottom: 10px;
`;

const LoginMessage = styled.div`
  position: relative;
  width: 100%;
  height: 200px;
  margin-right: auto;
  :hover {
    cursor: pointer;
  }
`;

const ButtonPositon = styled.div`
  position: absolute;
  width: 20%;
  bottom: 10px;
  right: 10px;
`;

export default function RegistrationScene(props) {
  const { buttonOnClick, loginFunction } = props;
  const [warnings, setWarnings] = useState([]);
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [address, setAddress] = useState('');
  const [postcode, setPostcode] = useState('');
  const [mobile, setMobile] = useState('');
  const [email, setEmail] = useState('');

  async function doRegister() {
    let newWarnings = [];

    if (userName === '') newWarnings.push('Username');
    if (password === '') newWarnings.push('Password');
    if (firstName === '') newWarnings.push('First Name');
    if (lastName === '') newWarnings.push('Last Name');
    if (address === '') newWarnings.push('Address');
    if (postcode === '') newWarnings.push('Postcode');
    if (mobile === '') newWarnings.push('Mobile Phone');
    if (email === '') newWarnings.push('Email');
    if (newWarnings.length > 0) return setWarnings(newWarnings);

    setWarnings(newWarnings);
    let response = await registerUser(
      userName,
      password,
      firstName,
      lastName,
      address,
      postcode,
      mobile,
      email
    );
    return buttonOnClick(response ? 'registersuccess' : 'registerfail');
  }
  return (
    <ContentArea>
      <RegistrationArea>
        <RegistrationMessage>Register</RegistrationMessage>
        <WarningMessage>
          {warnings.length > 0 && 'Please check the marked fields'}
        </WarningMessage>
        <RegistrationPositon>
          <FormInput
            showWarning={warnings.includes('Username')}
            inputKey={'Username'}
            inputValue={userName}
            updateFunction={setUserName}
          />
          <FormInput
            showWarning={warnings.includes('Password')}
            inputKey={'Password'}
            inputValue={password}
            updateFunction={setPassword}
          />
          <FormInput
            showWarning={warnings.includes('First Name')}
            inputKey={'First Name'}
            inputValue={firstName}
            updateFunction={setFirstName}
          />
          <FormInput
            showWarning={warnings.includes('Last Name')}
            inputKey={'Last Name'}
            inputValue={lastName}
            updateFunction={setLastName}
          />
          <FormInput
            showWarning={warnings.includes('Address')}
            inputKey={'Address'}
            inputValue={address}
            updateFunction={setAddress}
          />
          <FormInput
            showWarning={warnings.includes('Postcode')}
            inputKey={'Postcode'}
            inputValue={postcode}
            updateFunction={setPostcode}
          />
          <FormInput
            showWarning={warnings.includes('Mobile Phone')}
            inputKey={'Mobile Phone'}
            inputValue={mobile}
            updateFunction={setMobile}
          />
          <FormInput
            showWarning={warnings.includes('Email')}
            inputKey={'Email'}
            inputValue={email}
            updateFunction={setEmail}
          />
          <LoginPosition>
            <LoginMessage onClick={() => loginFunction()}>
              Already have an account? Click here to login.
            </LoginMessage>
          </LoginPosition>
          <ButtonPositon>
            <Button text={'Submit'} onClickFunc={() => doRegister()}></Button>
          </ButtonPositon>
        </RegistrationPositon>
      </RegistrationArea>
    </ContentArea>
  );
}
