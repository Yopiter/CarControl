#!/usr/bin/env bash
if [ -d /sys/class/gpio/gpio26 ]; then
echo "26" > /sys/class/gpio/unexport
fi
if [ -d /sys/class/gpio/gpio20 ]; then
echo "20" > /sys/class/gpio/unexport
fi
echo "26" > /sys/class/gpio/export
echo "20" > /sys/class/gpio/export
echo "out" > /sys/class/gpio/gpio26/direction
echo "1" > /sys/class/gpio/gpio26/value
echo "out" > /sys/class/gpio/gpio20/direction
echo "1" > /sys/class/gpio/gpio20/value
