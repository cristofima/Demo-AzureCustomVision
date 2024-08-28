export interface ImageAnalysis {
  caption: Caption;
  read: Read;
}

export interface Caption {
  confidence: number
  text: string
}

export interface Read {
  blocks: Block[]
}

export interface Block {
  lines: Line[]
}

export interface Line {
  text: string
}
