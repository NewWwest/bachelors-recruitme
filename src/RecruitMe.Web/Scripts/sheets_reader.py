#!/usr/bin/env python3
from imutils.perspective import four_point_transform
from imutils import contours
import numpy as np
import imutils
import cv2

def parse(imageName):
    answersInRow = 10
    showOutput=False

    # preprocess the image
    image = cv2.imread(imageName)
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
    blurred = cv2.GaussianBlur(gray, (5, 5), 0)
    edged = cv2.Canny(blurred, 75, 200)
    cnts = cv2.findContours(edged.copy(), cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
    cnts = imutils.grab_contours(cnts)
    docCnt = None
    if len(cnts) > 0:
        cnts = sorted(cnts, key=cv2.contourArea, reverse=True)
        for c in cnts:
            peri = cv2.arcLength(c, True)
            approx = cv2.approxPolyDP(c, 0.02 * peri, True)
            if len(approx) == 4:
                docCnt = approx
                break

    paper = four_point_transform(image, docCnt.reshape(4, 2))
    warped = four_point_transform(gray, docCnt.reshape(4, 2))
    resized = cv2.resize(warped, (1000, 1000))
    cropped = resized[5:resized.shape[0]-5, 5:resized.shape[1]-5]
    thresh = cv2.threshold(
        cropped, 0, 255, cv2.THRESH_BINARY_INV | cv2.THRESH_OTSU)[1]

    # find contours 
    cnts = cv2.findContours(thresh.copy(), cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
    cnts = imutils.grab_contours(cnts)
    questionCnts = []

    # loop over the contours
    for c in cnts:
        (x, y, w, h) = cv2.boundingRect(c)
        aspectRatio = w / float(h)
        #only left side of the document, only somewhat square bouding box, only specific size
        if x < 500 and aspectRatio >= 0.5 and aspectRatio <= 1.5 and w >= 15 and h >= 15 and w <= 25 and h <= 25:
            questionCnts.append(c)

    # loop over the points contours
    questionCnts = contours.sort_contours(questionCnts, method="top-to-bottom")[0]
    solution = np.zeros(int(len(questionCnts) / answersInRow))
    solutionCnts = []
    #loop over users
    for (q, i) in enumerate(np.arange(0, len(questionCnts), answersInRow)):
        cnts = contours.sort_contours(questionCnts[i:i + answersInRow])[0]
        bubbled = (-1, -1)

        # loop over points per users
        for (j, c) in enumerate(cnts):
            mask = np.zeros(thresh.shape, dtype="uint8")
            cv2.drawContours(mask, [c], -1, 255, -1)
            mask = cv2.bitwise_and(thresh, thresh, mask=mask)
            total = cv2.countNonZero(mask)

            # if more covered than override answer
            if bubbled is None or bubbled[0] < 0 or total > bubbled[0]:
                bubbled = (total, j)
                solution[q] = j
                bubbledCnt = c
        solutionCnts.append(bubbledCnt)

    if showOutput:
        for c in solutionCnts:
            cv2.drawContours(cropped, c, -1, (0, 0, 255), 3)
        cv2.imshow("Original", image)
        cv2.imshow("Processed", cropped)
        cv2.waitKey(0)

    print(solution)

if __name__ == '__main__':
    parse("temp")
