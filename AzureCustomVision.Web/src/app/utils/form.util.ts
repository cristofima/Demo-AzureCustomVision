export class FormUtil {
  static createFormData(image: File | Blob) {
    const formData = new FormData();
    formData.append('image', image, 'image.png');

    return formData;
  }
}
