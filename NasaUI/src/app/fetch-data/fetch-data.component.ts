import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public nasaData: NasaApodData[]=[];

  constructor(http: HttpClient) {
    http.get<NasaApodData[]>('https://localhost:7152/' + 'api/SampleData/NasaData').subscribe(result => {
      this.nasaData = result;
    }, error => console.error(error));
  }
}

interface NasaApodData {
  copyright: string;
  date: Date;
  url: number;
}
