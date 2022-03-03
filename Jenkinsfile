pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                echo 'Building..'
                dotnet build ./DiaryLog/DiaryLog.sln
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