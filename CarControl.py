#!/usr/bin/python
#coding: utf8
import sys
import pibrella
import time
import socket
import RPi.GPIO as GPIO

left=0
right=0
lMPin=20
rMPin=26

#GPIOs für Relais initialisieren
#GPIO.setwarnings(False)
GPIO.setmode(GPIO.BCM)
GPIO.setup(lMPin, GPIO.OUT) #linker Motor, Relais 3/4
GPIO.setup(rMPin, GPIO.OUT) #rechter Motor, Relais 1/2

s=socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind(("127.0.0.1",1338))
s.listen(999999999)

def FehlerDetected():
    #Fehler! Mysterious Shit!
    print 'Fehler'
    pibrella.output.off()
    pibrella.light.green.off()
    pibrella.light.red.blink(0.5,0.5)
    time.sleep(2)
    pibrella.light.red.off()

def WaitForCommand():
    command=s.recv(5)
    left=command.split(':')[0]
    right=command.split(':')[1]

def DoStuff():
    pibrella.light.green.on()
    print left, right
    # e... linker Motor
    # h... rechter Motor
    if left=="+1":
        pibrella.output.f.off()
        GPIO.output(lMPin, GPIO.HIGH)
        pibrella.output.e.on()
    elif left=="+0":
        pibrella.output.e.off()
        GPIO.output(lMPin, GPIO.HIGH)
        pibrella.output.f.off()
    elif left=="-1":
        pibrella.output.f.on()
        GPIO.output(lMPin, GPIO.LOW)
        pibrella.output.e.on()
    else:
        FehlerDetected()

    if right=="+1":
        pibrella.output.g.off()
        GPIO.output(rMPin, GPIO.HIGH)
        pibrella.output.h.on()
    elif right=="+0":
        pibrella.output.g.off()
        GPIO.output(rMPin, GPIO.HIGH)
        pibrella.output.h.off()
    elif right=="-1":
        pibrella.output.g.on()
        GPIO.output(rMPin, GPIO.LOW) 
        pibrella.output.h.on()
    else:
        FehlerDetected()

    pibrella.light.green.off()

try:
    while 1:
        cliSock, addr=s.accept()
        left=cliSock.recv(2).decode('utf-8')
        right=cliSock.recv(2).decode('utf-8')
        print left
        print right
        DoStuff()
except:
    cliSock.close()
    s.close()
    GPIO.cleanup()
    print "tot"
