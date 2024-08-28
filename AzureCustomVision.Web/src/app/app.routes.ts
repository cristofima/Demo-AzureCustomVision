import { Routes } from '@angular/router';
import { ObjectDetectionComponent } from './components/object-detection/object-detection.component';
import { ImageClassificationComponent } from './components/image-classification/image-classification.component';
import { ImageAnalysisComponent } from './components/image-analysis/image-analysis.component';

export const routes: Routes = [
  {
    path: 'object-detection',
    component: ObjectDetectionComponent
  },
  {
    path: 'image-classification',
    component: ImageClassificationComponent
  },
  {
    path: 'image-analysis',
    component: ImageAnalysisComponent
  },
  {
    path: '',
    redirectTo: '/image-classification',
    pathMatch: 'full'
  }

];
