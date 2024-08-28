import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormUtil } from '@/utils/form.util';
import { ObjectDetection } from '@/models/object-detection.model';
import { ImageAnalysis } from '@/models/image-analysis.model';
import { environment } from '../../environments/environment';
import { Prediction } from '@/models/prediction.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }

  classifyPlantDisease(image: File) {
    const formData = FormUtil.createFormData(image);
    return this.http.post<Prediction[]>(`${this.baseUrl}/Classification/PlantDiseases`, formData);
  }

  detectCarPlate(image: File) {
    const formData = FormUtil.createFormData(image);
    return this.http.post<ObjectDetection[]>(`${this.baseUrl}/ObjectDetection/CarPlate`, formData);
  }

  analyzeImage(image: File | Blob, features: string) {
    const formData = FormUtil.createFormData(image);
    return this.http.post<ImageAnalysis>(`${this.baseUrl}/ImageAnalysis?features=${features}`, formData);
  }
}
