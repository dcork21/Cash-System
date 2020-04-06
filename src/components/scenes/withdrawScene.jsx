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

const WithdrawArea = styled.div`
  height: 50%;
  width: 80%;
  margin-left: auto;
  margin-right: auto;
  border: 1px solid white;
  border-radius: 5px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  padding: 20px;
`;
const WithdrawMessage = styled.div`
  position: relative;
  width: 100%;
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
const Amount = styled.div`
  margin-left: auto;
  margin-right: auto;
`;
const ButtonPositon = styled.div`
  width: 100%;
  height: 20%;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 10%;
  display: flex;
  justify-content: space-evenly;
`;
export default function MainMenuScene(props) {
  const { amountClick, confirmClick, amount } = props;
  return (
    <ContentArea>
      <WithdrawArea>
        <WithdrawMessage>Cash Withdrawal</WithdrawMessage>
        <Amount>{`£${amount}`}</Amount>
        <ButtonPositon>
          <Button
            text={'- £5'}
            buttonKey={'withdraw'}
            onClickFunc={() => amountClick(-5)}
          ></Button>
          <Button
            text={'+ £5'}
            buttonKey={'withdraw'}
            onClickFunc={() => amountClick(5)}
          ></Button>
          <Button
            text={'Confirm'}
            buttonKey={'verification'}
            onClickFunc={confirmClick}
          ></Button>
        </ButtonPositon>
      </WithdrawArea>
    </ContentArea>
  );
}
