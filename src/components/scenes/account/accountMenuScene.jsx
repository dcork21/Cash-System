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

const MenuArea = styled.div`
  height: 230px;
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
const MenuMessage = styled.div`
  position: relative;
  width: 90%;
  height: 30%;
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

export default function AccountMenuScene(props) {
  const { buttonOnClick, accounts } = props;
  return (
    <ContentArea>
      <MenuArea>
        <MenuMessage>Your Bank Accounts</MenuMessage>
        {accounts && accounts.map(a => <Button
          text={a.bank.bankName}
          buttonKey={'manageaccount'}
          onClickProps={a}
          onClickFunc={buttonOnClick}
        />)}
        <Button
          text={'Register New Account'}
          buttonKey={'registeraccount'}
          onClickFunc={buttonOnClick}
        />
        <Button
          text={'Back to Menu'}
          buttonKey={'mainmenu'}
          onClickFunc={buttonOnClick}
        />
      </MenuArea>
    </ContentArea>
  );
}
