pipeline {
    agent any
    
    stages {
        stage('Build') {
            steps {
                echo 'Building..'
				
                sh 'dotnet build DiaryLog/DiaryLog.sln'
				
				dir('diary-log-angular') {
					sh 'npm install'
					sh 'npx ng build diary-log-angular'
				}
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
    }
}