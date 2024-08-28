export interface ObjectDetection {
  probability: number;
  boundingBox: BoundingBox;
}

export interface BoundingBox {
  left: number;
  top: number;
  width: number;
  height: number;
}
