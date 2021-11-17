import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Post } from '../models/Post';
import { PostDetails } from '../models/PostDetails';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private url: string = "api/posts";

  constructor(private http: HttpClient) { }

  getAll(): Observable<Post[]> {
    return this.http.get<Post[]>(this.url);
  }

  getById(id: number): Observable<PostDetails> {
    const getUrl = `${this.url}/${id}`;
    return this.http.get<PostDetails>(getUrl);
  }

  getAllSorted(sortBy: string, sortOrder: string): Observable<Post[]> {
    const fullUrl = `${this.url}?sortBy=${sortBy}&sortOrder=${sortOrder}`;
    return this.http.get<Post[]>(fullUrl);
  }

  update(post: Post): Observable<any> {
    return this.http.put<any>(this.url, post, httpOptions);
  }

  delete(id: number): Observable<any> {
    const delUrl = `${this.url}/${id}`;
    return this.http.delete(delUrl, httpOptions);
  }
}
