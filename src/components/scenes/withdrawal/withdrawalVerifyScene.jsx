import React, { useEffect, useState, useCallback, Fragment } from 'react';
import styled from 'styled-components';
import Button from '../../generic/button';
import DateCountdown from 'react-date-countdown-timer';
import { confirmWithdrawal } from '../../../requests/withdrawalRequests';

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
  height: 30%;
  width: 75%;
  margin-left: auto;
  margin-right: auto;
  border: 1px solid white;
  border-radius: 5px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  font-family: ${'Calibri (Body)'};
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
  font-size: 14px;
  font-family: ${'Calibri (Body)'};
  font-weight: ${'Bold'};
`;
const CountdownMessage = styled.div`
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
  font-size: 14px;
  font-family: ${'Calibri (Body)'};
  font-weight: ${'Bold'};
`;
const AmountMessage = styled.div`
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
  font-size: 22px;
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
export default function VerificationScene(props) {
  const {
    confirmedWithdrawal,
    buttonOnClick,
    accountId,
    withdrawalToken,
    expiry,
    amount,
  } = props;

  // eslint-disable-next-line react-hooks/exhaustive-deps
  const calculateTimeLeft = useCallback(() => {
    const difference = +new Date(expiry) - +new Date();
    let timeLeft = {};

    if (difference > 0) {
      timeLeft = {
        days: Math.floor(difference / (1000 * 60 * 60 * 24)),
        hours: Math.floor((difference / (1000 * 60 * 60)) % 24),
        minutes: Math.floor((difference / 1000 / 60) % 60),
        seconds: Math.floor((difference / 1000) % 60),
      };
    }

    return timeLeft;
  });
  const [timeLeft, setTimeLeft] = useState(calculateTimeLeft());
  const [hasWithdrawn, setHasWithdrawn] = useState(false);

  useEffect(() => {
    setTimeout(() => {
      setTimeLeft(calculateTimeLeft());
    }, 1000);
    return () => {};
  }, [expiry, calculateTimeLeft]);

  const timerComponents = [];

  Object.keys(timeLeft).forEach((interval) => {
    if (!timeLeft[interval]) {
      return;
    }

    timerComponents.push(
      <span>
        {timeLeft[interval]} {interval}{' '}
      </span>
    );
  });

  async function mockNfcTransaction() {
    const response = await confirmWithdrawal(accountId, withdrawalToken);
    console.log('response', response);
    if (response) return setHasWithdrawn(true);
    return confirmedWithdrawal(withdrawalToken);
  }
  return (
    <ContentArea>
      <MenuArea>
        {!hasWithdrawn ? (
          <Fragment>
            <MenuMessage>{withdrawalToken}</MenuMessage>
            <AmountMessage>{`£${amount}`}</AmountMessage>
            <CountdownMessage>
              <DateCountdown
                dateTo={expiry}
                callback={() => alert('Withdrawal Request Expired')}
              />
            </CountdownMessage>
            <ButtonPositon>
              <Button
                text={'Mock NFC Transaction'}
                onClickFunc={mockNfcTransaction}
              />
            </ButtonPositon>
          </Fragment>
        ) : (
          <MenuMessage>Transaction Complete</MenuMessage>
        )}
        <ButtonPositon>
          <Button
            text={'Return to Main Menu'}
            buttonKey={'mainmenu'}
            onClickFunc={buttonOnClick}
          ></Button>
        </ButtonPositon>
      </MenuArea>
    </ContentArea>
  );
}
