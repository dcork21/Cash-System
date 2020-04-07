import React, { useState } from 'react';
import styled from 'styled-components';
import Button from '../generic/button';
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
  height: 300px;
  width: 40%;
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
  width: 60%;
  height: 50px;
  margin-left: auto;
  margin-right: auto;
  background-color: #9e9e9e;
  box-sizing: border-box;
  display: flex;
  justify-content: center;
  text-align: center;
  font-size: 24px;
  font-family: ${'Calibri (Body)'};
  font-weight: ${'Bold'};
`;
const LoginPositon = styled.div`
  width: 100%;
  height: 200px;
  margin-left: auto;
  margin-right: auto;
`;

const LoginBox = styled.div`
  margin-left: auto;
  margin-right: auto;
  width: 40%;
  display: flex;
`;
const LoginKey = styled.div`
  margin-left: 20px;
  margin-top: 10px;
`;
const LoginInput = styled.input`
  margin-left: 10px;
  margin-top: 10px;
`;

const ButtonPositon = styled.div`
  width: 40%;
  height: 20%;
  margin-top: 10px;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 10%;
`;
export default function LoginScene(props) {
  const [userName, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const { buttonOnClick } = props;
  return (
    <ContentArea>
      <LoginArea>
        <LoginMessage>Login to your account</LoginMessage>
        <LoginPositon>
          <LoginBox>
            <LoginKey>Username</LoginKey>
            <LoginInput onChange={(e) => setUsername(e.target.value)} />
          </LoginBox>
          <LoginBox>
            <LoginKey>Password</LoginKey>
            <LoginInput
              onChange={(e) => setPassword(e.target.value)}
              type={'password'}
            />
          </LoginBox>
          <ButtonPositon>
            <Button
              text={'Submit'}
              onClickFunc={() => buttonOnClick(userName, password)}
            ></Button>
          </ButtonPositon>
        </LoginPositon>
      </LoginArea>
    </ContentArea>
  );
}
