export interface ImageAnalysis {
  caption: Caption;
  denseCaptions?: {
    values: Caption[];
  };
  read?: Read;
  people?: {
    values: PersonDetection[];
  };
  tags?: {
    values: TagPrediction[];
  };
}

export interface Caption {
  confidence: number;
  text: string;
}

export interface Read {
  blocks: Block[];
}

export interface Block {
  lines: Line[];
}

export interface Line {
  text: string;
}

export interface PersonDetection {
  boundingBox: BoundingBox;
  confidence: number;
}

export interface BoundingBox {
  x: number;
  y: number;
  width: number;
  height: number;
}

export interface TagPrediction {
  name: string;
  confidence: number;
}
