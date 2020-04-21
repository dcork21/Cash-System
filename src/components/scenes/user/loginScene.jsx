import React, { useState } from 'react';
import styled from 'styled-components';
import Button from '../../generic/button';
import FormInput from '../../generic/formInput';
import { authenticateUser } from '../../../requests/userRequests';

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

const LoginArea = styled.div`
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
const LoginMessage = styled.div`
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

const LoginPosition = styled.div`
  position: relative;
  width: 100%;
  height: auto;
  margin-right: auto;
  padding-bottom: 50px;
`;
const RegisterPosition = styled.div`
  position: absolute;
  width: 60%;
  height: 20px;
  left: 10px;
  bottom: 10px;
`;
const RegisterMessage = styled.div`
  position: relative;
  width: 100%;
  height: 200px;
  margin-right: auto;
  font-size: 12px;
  :hover {
    cursor: pointer;
  }
`;

const ButtonPositon = styled.div`
  position: absolute;
  width: 30%;
  bottom: 5px;
  right: 5px;
`;

export default function LoginScene(props) {
  const { loginSuccessFunction, registerFunction } = props;
  const [hasFailed, setHasFailed] = useState(false);
  const [warnings, setWarnings] = useState([]);
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');

  async function doLogin() {
    let newWarnings = [];

    if (userName === '') newWarnings.push('Username');
    if (password === '') newWarnings.push('Password');
    setWarnings(newWarnings);
    if (newWarnings.length > 0) return;

    let response = await authenticateUser(userName, password);
    if (response)
      return loginSuccessFunction(response);
    return setHasFailed(true);
  }

  return (
    <ContentArea>
      <LoginArea>
        <LoginMessage>Login</LoginMessage>
        <WarningMessage>
          {hasFailed && 'Username or Password incorrect'}
        </WarningMessage>
        <LoginPosition>
          <FormInput
            showWarning={warnings.includes('Username')}
            inputKey={'Username'}
            inputValue={userName}
            updateFunction={setUserName}
          />
          <FormInput
            showWarning={warnings.includes('Password')}
            inputType={'password'}
            inputKey={'Password'}
            inputValue={password}
            updateFunction={setPassword}
          />
          <RegisterPosition>
            <RegisterMessage onClick={() => registerFunction()}>
              No account? Register here.
            </RegisterMessage>
          </RegisterPosition>
          <ButtonPositon>
            <Button text={'Submit'} onClickFunc={() => doLogin()}></Button>
          </ButtonPositon>
        </LoginPosition>
      </LoginArea>
    </ContentArea>
  );
}
