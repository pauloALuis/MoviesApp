import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'MoviesApp';
  http = inject (HttpClient);
    movies:  any;
  //constructor (private httpClient: HttpClient) {} 
  
  
  ngOnInit(): void {
     // throw new Error('Method not implemented.');
     this.http.get('https://localhost:5006/api/movies').subscribe({
                  next: response => this.movies = response, // ()=> {},
                  error: error => console.log(error),
                  complete: ()=> console.log('Resquest has completed')      
                });
    }
    
}
