import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public nasaData: NasaApodData[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<NasaApodData[]>(baseUrl + 'api/SampleData/NasaData').subscribe(result => {
      this.nasaData = result;
    }, error => console.error(error));
  }
}

interface NasaApodData {
  copyright: string;
  date: Date;
  url: number;
}
