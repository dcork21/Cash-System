import React from 'react';
import styled from 'styled-components';
import Button from '../../generic/button';
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
  height: 350px;
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
const WelcomeTitle = styled.div`
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
  font-size: 16px;
  font-family: ${'Calibri (Body)'};
  font-weight: ${'Bold'};
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
  font-size: 14px;
  font-family: ${'Calibri (Body)'};
  font-weight: ${'Bold'};
`;

const ButtonPositon = styled.div`
  width: 90%;
  height: 200px;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 10px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
`;
export default function AccountScene(props) {
  const { buttonOnClick, bank, sortCode, accountNumber, balance } = props;
  return (
    <ContentArea>
      <WelcomeArea>
        <WelcomeTitle>{`Your ${bank.bankName} Account`}</WelcomeTitle>
        <WelcomeMessage>{`SortCode: ${sortCode}`}</WelcomeMessage>
        <WelcomeMessage>{`Account Number: ${accountNumber}`}</WelcomeMessage>
        <WelcomeMessage>{`Balance: £${balance.toFixed(2)}`}</WelcomeMessage>
        <ButtonPositon>
          <Button
            text={'Request Withdraw'}
            buttonKey={'withdraw'}
            onClickProps={props}
            onClickFunc={buttonOnClick}
          />
          <Button
            text={'Back to Accounts Menu'}
            buttonKey={'accountmenu'}
            onClickFunc={buttonOnClick}
          />
          <Button
            text={'Back to Main Menu'}
            buttonKey={'mainmenu'}
            onClickFunc={buttonOnClick}
          />
        </ButtonPositon>
      </WelcomeArea>
    </ContentArea>
  );
}
