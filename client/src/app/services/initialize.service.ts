import { Injectable } from '@angular/core';
import { Observable, } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Message } from '../models/Message';


@Injectable({
  providedIn: 'root'
})
export class InitializeService {

  private url: string = "api/initialize";

  constructor(private http: HttpClient) { }

  reloadPosts() : Observable<Message>{
    return this.http.get<Message>(this.url)
  }
}
