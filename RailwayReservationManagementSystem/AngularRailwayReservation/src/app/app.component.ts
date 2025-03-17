import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  title = 'AngularRailwayReservation';
data :any =[];

constructor(private authService : AuthService){

}

ngOnInit(): void {
  this.authService.getData().subscribe({
    next: (response) => {
      console.log(response);
    },
    error: (error) =>{
      console.log(error);
    },
    complete:() =>{
      
    }
  })
}

}
