import React from 'react';
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

const WelcomeArea = styled.div`
  height: 20%;
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
const WelcomeMessage = styled.div`
  position: relative;
  width: 90%;
  height: 20%;
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
const ButtonPositon = styled.div`
  width: 100%;
  height: 20%;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 10%;
`;
export default function InitialScene(props) {
  const { buttonOnClick } = props;
  return (
    <ContentArea>
      <WelcomeArea>
        <WelcomeMessage>Welcome to The Smart Bank</WelcomeMessage>
        <ButtonPositon>
          <Button
            text={'Continue'}
            buttonKey={'mainmenu'}
            onClickFunc={buttonOnClick}
          ></Button>
        </ButtonPositon>
      </WelcomeArea>
    </ContentArea>
  );
}
