import React from 'react';
import styled from 'styled-components';
import Button from '../../generic/button';
import { useState } from 'react';
import { createWithdrawal } from '../../../requests/withdrawalRequests';
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
const WithdrawWarning = styled.div`
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
  font-size: 18px;
  font-family: ${'Calibri (Body)'};
  color: red;
`;
const Amount = styled.div`
  margin-left: auto;
  margin-right: auto;
  font-size: 30px;
  font-weight: 600;
`;
const AmountButtonPositon = styled.div`
  width: 30%;
  height: 20%;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 10%;
  display: flex;
  justify-content: space-evenly;
  box-sizing: border-box;
`;

const MenuButtonPositon = styled.div`
  width: 100%;
  height: 20%;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 10%;
  display: flex;
  justify-content: space-evenly;
`;
export default function WithdrawScene(props) {
  const {
    updateWithdrawals,
    accountId,
    sessionToken,
    accountNumber,
    buttonOnClick,
  } = props;
  const [hasFailed, setHasFailed] = useState(false);
  const [amount, setAmount] = useState(10);
  function setWithdrawAmount(newAmount) {
    if ((amount > 5 && newAmount < 0) || (amount < 250 && newAmount > 0))
      return setAmount(amount + newAmount);
  }

  async function confirmWithdrawal() {

    const response = await createWithdrawal(
      accountId,
      sessionToken,
      accountNumber,
      amount
    );

    if (!response) return setHasFailed(true);
    return updateWithdrawals({ ...response, accountId, amount });
  }
  return (
    <ContentArea>
      <WithdrawArea>
        <WithdrawMessage>Cash Withdrawal</WithdrawMessage>
        {hasFailed && (
          <WithdrawWarning>Withdrawal request unsuccessful</WithdrawWarning>
        )}
        <Amount>{`£${amount}`}</Amount>
        <AmountButtonPositon>
          <Button
            text={'- £5'}
            buttonKey={'withdraw'}
            onClickFunc={() => setWithdrawAmount(-5)}
          ></Button>
          <Button
            text={'+ £5'}
            buttonKey={'withdraw'}
            onClickFunc={() => setWithdrawAmount(5)}
          ></Button>
        </AmountButtonPositon>
        <MenuButtonPositon>
          <Button
            text={'Back to Menu'}
            buttonKey={'mainmenu'}
            onClickFunc={buttonOnClick}
          ></Button>
          <Button
            text={'Confirm'}
            onClickFunc={() => confirmWithdrawal()}
          ></Button>
        </MenuButtonPositon>
      </WithdrawArea>
    </ContentArea>
  );
}
